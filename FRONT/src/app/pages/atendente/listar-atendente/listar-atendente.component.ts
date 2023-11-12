import { Component } from '@angular/core';
import { Atendente } from 'src/app/models/atendente.models';
import { HttpClient } from "@angular/common/http";
import { MatSnackBar } from "@angular/material/snack-bar";
import { Router } from '@angular/router';

@Component({
  selector: 'app-listar-atendente',
  templateUrl: `./listar-atendente.component.html`,
  styleUrls: ["./listar-atendente.component.css"],
})
export class ListarAtendenteComponent {
  colunasTabela : string[] = [
    "id",
    "nome",
    "deletar",
    // "alterar",
  ];

  atendentes: Atendente[] = [];

  constructor( private client: HttpClient, 
    private snackBar: MatSnackBar,
    private router: Router,
    ) {}

  ngOnInit(): void {
    this.client
    .get<Atendente[]>("https://localhost:7288/api/atendente/listar")
    .subscribe({
      //funcionou
      next: (atendentes) => {
        this.atendentes = atendentes;
        console.log(atendentes);
      },
      // não funcionou
      error: (erro) => {
        console.log(erro);
      }
    })
  }

  deletar(atendenteId: number) {
    this.client
    .delete<Atendente[]>(
      `https://localhost:7288/api/atendente/deletar/${atendenteId}`
    )
    .subscribe({
      next: (atendentes) => {
        this.atendentes = atendentes;
        this.snackBar.open("Atendente deletado com sucesso!", "OK", {
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
