import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Carrinho } from 'src/app/models/carrinho.models';

@Component({
  selector: 'app-listar-carrinho',
  templateUrl: "./listar-carrinho.component.html",
  styleUrls: ["./listar-carrinho.component.css"],
})
export class ListarCarrinhoComponent {
  colunasTabela: string[] = [
    "id",
    "cliente",
    "cardapio",
    "quantidade",
    "totalPedido",
    "criadoEm",
    "deletar",
    "finalizar",
  ];

  carrinhos: Carrinho[] = [];

  constructor(private client: HttpClient, private snackBar: MatSnackBar) {}

  //Método que é executado ao abrir um componente
  ngOnInit(): void {
    this.client
      .get<Carrinho[]>("https://localhost:7288/api/carrinho/listar")
      .subscribe({
        //A requição funcionou
        next: (carrinhos) => {
          this.carrinhos = carrinhos;
        },
        //A requição não funcionou
        error: (erro) => {
          console.log(erro);
        },
      });
  }


  deletar(carrinhoId: number) {
    this.client
      .delete<Carrinho[]>(
        `https://localhost:7288/api/carrinho/deletar/${carrinhoId}`
      )
      .subscribe({
        //Requisição com sucesso
        next: (carrinhos) => {
          this.carrinhos = carrinhos;
          this.snackBar.open("Pedido deletado com sucesso!!", "OK", {
            duration: 2000,
            horizontalPosition: "left",
            verticalPosition: "top",
          });
          this.ngOnInit();
        },
        //Requisição com erro
        error: (erro) => {
          console.log(erro);
        },
      });
  }

  finalizarPedido(carrinhoId: number) {
    this.client.patch(`https://localhost:7288/api/carrinho/finalizar/${carrinhoId}`, {}).subscribe({
      next: (resposta) => {
        console.log('Pedido finalizado com sucesso', resposta);
        this.ngOnInit(); // Atualize a lista de carrinhos após finalizar
        this.snackBar.open('Pedido finalizado com sucesso!!', 'OK', {
          duration: 2000,
          horizontalPosition: 'left',
          verticalPosition: 'top',
        });
      },
      error: (erro) => {
        console.error('Erro ao finalizar o pedido', erro);
        this.snackBar.open('Pedido já finalizado com sucesso!!', 'OK', {
          duration: 2000,
          horizontalPosition: 'left',
          verticalPosition: 'top',
        });
      },
    });
  }

}
