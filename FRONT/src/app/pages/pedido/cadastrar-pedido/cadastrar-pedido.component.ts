import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { Atendente } from 'src/app/models/atendente.models';
import { Cardapio } from 'src/app/models/cardapio.models';
import { Carrinho } from 'src/app/models/carrinho.models';
import { Cliente } from 'src/app/models/cliente.models';
import { Pedido } from 'src/app/models/pedido.models';

@Component({
  selector: 'app-cadastrar-pedido',
  templateUrl: "./cadastrar-pedido.component.html",
  styleUrls: ["./cadastrar-pedido.component.css"],
})
export class CadastrarPedidoComponent {
  atendenteId: number = 0;
  atendentes: Atendente[] = [];
  clienteId: number = 0;
  clientes: Cliente[] = [];
  cardapioId: number = 0;
  cardapios: Cardapio[] = [];
  carrinhoId: number = 0;
  carrinhos: Carrinho[] = [];
  totalPedido: string = "";

  constructor(
    private client: HttpClient,
    private router: Router,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.client
      .get<Atendente[]>("https://localhost:7288/api/atendente/listar")
      .subscribe({
        //A requição funcionou
        next: (atendentes) => {
          this.atendentes = atendentes;
        },
        //A requição não funcionou
        error: (erro) => {
          console.log(erro);
        },
      });

      this.client
        .get<Carrinho[]>("https://localhost:7288/api/carrinho/listar")
        .subscribe({
          next: (carrinhos) => {
            this.carrinhos = carrinhos;
          },
          error: (erro) => {
            console.log(erro);
          }
        })
  }

  cadastrar(): void {
    let pedido: Pedido = {
      atendenteId: this.atendenteId,
      carrinhoId: this.carrinhoId,
      clienteId: this.clienteId,
      cardapioId: this.cardapioId,
      totalPedido: Number.parseFloat(this.totalPedido),

    };

    this.client
      .post<Pedido>("https://localhost:7288/api/pedido/cadastrar", pedido)
      .subscribe({
        //A requição funcionou
        next: (pedido) => {
          this.snackBar.open("Pedido cadastrado com sucesso!!", "OK", {
            duration: 2000,
            horizontalPosition: "left",
            verticalPosition: "top",
          });
          this.router.navigate(["pages/pedido/listar-pedido"]);
        },
        //A requição não funcionou
        error: (erro) => {
          console.log(erro);
        },
      });
  }
}