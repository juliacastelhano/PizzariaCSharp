import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { Atendente } from 'src/app/models/atendente.models';

@Component({
  selector: 'app-alterar-atendente',
  templateUrl: "./alterar-atendente.component.html" ,
  styleUrls: ["./alterar-atendente.component.css"],
})
export class AlterarAtendenteComponent {
  atendenteId: number = 0;
  nome: string = "";

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
        this.client.get<Atendente>(`https://localhost:7288/api/atendente/buscar/${id}`).subscribe({
          next: (atendente) => {
            this.atendenteId = atendente.atendenteId!;
            this.nome = atendente.nome;
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
    let atendente: Atendente = {
      nome: this.nome,
    };

    console.log(atendente);

    this.client.put<Atendente>(`https://localhost:7288/api/atendente/alterar/${this.atendenteId}`, atendente).subscribe({
      //A requição funcionou
      next: (atendente) => {
        this.snackBar.open("Atendente alterado com sucesso!!", "OK", {
          duration: 2000,
          horizontalPosition: "left",
          verticalPosition: "top",
        });
        this.router.navigate(["pages/atendente/listar-atendente"]);
      },
      //A requição não funcionou
      error: (erro) => {
        console.log(erro);
      },
    });
  }

}
