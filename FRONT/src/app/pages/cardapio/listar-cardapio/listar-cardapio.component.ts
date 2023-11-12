import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Cardapio } from 'src/app/models/cardapio.models';

@Component({
  selector: 'app-listar-cardapio',
  templateUrl: "./listar-cardapio.component.html",
  styleUrls: ["./listar-cardapio.component.css"]
})
export class ListarCardapioComponent {
  colunasTabela : string[] = [
    "id",
    "sabor",
    "descricao",
    "preco",
    "deletar",
    // "alterar",
  ];

  cardapios: Cardapio[] = [];

  constructor( private client: HttpClient, private snackBar: MatSnackBar) {}

  ngOnInit(): void {
    this.client
    .get<Cardapio[]>("https://localhost:7288/api/cardapio/listar")
    .subscribe({
      //funcionou
      next: (cardapios) => {
        this.cardapios = cardapios;
        console.log(cardapios);
      },
      // não funcionou
      error: (erro) => {
        console.log(erro);
      }
    })
  }

  deletar(cardapioId: number) {
    this.client
    .delete<Cardapio[]>(
      `https://localhost:7288/api/cardapio/deletar/${cardapioId}`
    )
    .subscribe({
      next: (cardapios) => {
        this.cardapios = cardapios;
        this.snackBar.open("Pizza deletada com sucesso!", "OK", {
          duration: 2000,
          horizontalPosition: "left",
          verticalPosition: "top",
        });
        this.ngOnInit();
      },

      error: (erro) => {
        console.log(erro);
      }
    })
  }
}
