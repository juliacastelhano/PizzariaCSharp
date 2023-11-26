import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { Cardapio } from 'src/app/models/cardapio.models';
import { Carrinho } from 'src/app/models/carrinho.models';
import { Cliente } from 'src/app/models/cliente.models';

@Component({
  selector: 'app-cadastrar-carrinho',
  templateUrl: "./cadastrar-carrinho.component.html",
  styleUrls: ["./cadastrar-carrinho.component.css"],
})
export class CadastrarCarrinhoComponent {
  clienteId: number = 0;
  clientes: Cliente[] = [];
  cardapioId: number = 0;
  cardapios: Cardapio[] = [];
  quantidade: string = "";
  totalPedido: string = "";

  constructor(
    private client: HttpClient,
    private router: Router,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.client
      .get<Cliente[]>("https://localhost:7288/api/cliente/listar")
      .subscribe({
        //A requição funcionou
        next: (clientes) => {
          this.clientes = clientes;
        },
        //A requição não funcionou
        error: (erro) => {
          console.log(erro);
        },
      });

      this.client
        .get<Cardapio[]>("https://localhost:7288/api/cardapio/listar")
        .subscribe({
          next: (cardapios) => {
            this.cardapios = cardapios;
          },
          error: (erro) => {
            console.log(erro);
          }
        })
  }

  cadastrar(): void {
    let carrinho: Carrinho = {
      clienteId: this.clienteId,
      cardapioId: this.cardapioId,      
      quantidade: Number.parseInt(this.quantidade),
      // totalPedido: Number.parseFloat(this.totalPedido),

    };

    this.client
      .post<Carrinho>("https://localhost:7288/api/carrinho/cadastrar", carrinho)
      .subscribe({
        //A requição funcionou
        next: (carrinho) => {
          this.snackBar.open("Pedido cadastrado com sucesso!!", "OK", {
            duration: 2000,
            horizontalPosition: "left",
            verticalPosition: "top",
          });
          this.router.navigate(["pages/carrinho/listar-carrinho"]);
        },
        //A requição não funcionou
        error: (erro) => {
          console.log(erro);
        },
      });
  }
}
