import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { Atendente } from 'src/app/models/atendente.models';

@Component({
  selector: 'app-cadastrar-atendente',
  templateUrl: "./cadastrar-atendente.component.html",
  styleUrls: ["./cadastrar-atendente.component.css"],
})
export class CadastrarAtendenteComponent {
  nome: string = "";


  constructor(
    private client: HttpClient,
    private router: Router,
    private snackBar: MatSnackBar
  ) {}

  cadastrar(): void {
    let atendente: Atendente = {
      nome: this.nome,
    };

    this.client
      .post<Atendente>("https://localhost:7288/api/atendente/cadastrar", atendente)
      .subscribe({

        next: (atendente) => {
          this.snackBar.open("Atendente cadastrado com sucesso!!", "OK", {
            duration: 2000,
            horizontalPosition: "left",
            verticalPosition: "top",
          });
          this.router.navigate(["pages/atendente/listar-atendente"]);
        },

        error: (erro) => {
          console.log(erro);
        },
      });
  }
}
