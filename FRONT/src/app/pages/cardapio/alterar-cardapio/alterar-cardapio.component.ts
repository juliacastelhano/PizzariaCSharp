import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { Cardapio } from 'src/app/models/cardapio.models';

@Component({
  selector: 'app-alterar-cardapio',
  templateUrl: "./alterar-cardapio.component.html" ,
  styleUrls: ["./alterar-cardapio.component.css"],
})
export class AlterarCardapioComponent {
  cardapioId: number = 0;
  sabor: string = "";
  descricao: string = "";
  preco: number = 0;

  constructor(
    private client: HttpClient,
    private router: Router,
    private snackBar: MatSnackBar,
    private route: ActivatedRoute
  ) {}

 
  ngOnInit(): void {
    this.route.params.subscribe({
      next: (parametros) => {
        let { id } = parametros;
        this.client.get<Cardapio>(`https://localhost:7288/api/cardapio/buscar/${id}`).subscribe({
          next: (cardapio) => {
            this.cardapioId = cardapio.cardapioId!;
            this.sabor = cardapio.sabor;
            this.descricao = cardapio.descricao;
            this.preco = cardapio.preco;
          },
          //Requisição com erro
          error: (erro) => {
            console.log(erro);
          },
        });
      },
    });
  }

  alterar(): void {
    let cardapio: Cardapio = {
      sabor: this.sabor,
      descricao: this.descricao,
      preco: this.preco

    };

    console.log(cardapio);

    this.client.put<Cardapio>(`https://localhost:7288/api/cardapio/alterar/${this.cardapioId}`, cardapio).subscribe({
      //A requição funcionou
      next: (cardapio) => {
        this.snackBar.open("Cardapio alterado com sucesso!!", "OK", {
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
