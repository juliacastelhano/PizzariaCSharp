import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { Cardapio } from 'src/app/models/cardapio.models';

@Component({
  selector: 'app-cadastrar-cardapio',
  templateUrl: "./cadastrar-cardapio.component.html",
  styleUrls: ["./cadastrar-cardapio.component.css"]
})
export class CadastrarCardapioComponent {
  sabor: string = "";
  descricao: string = "";
  preco: string = "";

  constructor(
    private client: HttpClient,
    private router: Router,
    private snackBar: MatSnackBar
  ) {}

  
  cadastrar(): void {
    let cardapio: Cardapio = {
      sabor: this.sabor,
      descricao: this.descricao,
      preco: Number.parseFloat(this.preco),
    };

    this.client
      .post<Cardapio>("https://localhost:7288/api/cardapio/cadastrar", cardapio)
      .subscribe({
        //A requição funcionou
        next: (cardapio) => {
          this.snackBar.open("Pizza cadastrada com sucesso!!", "OK", {
            duration: 2000,
            horizontalPosition: "left",
            verticalPosition: "top",
          });
          this.router.navigate(["pages/cardapio/listar-cardapio"]);
        },
        //A requição não funcionou
        error: (erro) => {
          console.log(erro);
        },
      });
  }
}
