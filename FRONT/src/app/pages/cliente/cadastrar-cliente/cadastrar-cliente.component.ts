import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { Cliente } from 'src/app/models/cliente.models';

@Component({
  selector: 'app-cadastrar-cliente',
  templateUrl: "./cadastrar-cliente.component.html",
  styleUrls: ["./cadastrar-cliente.component.css"]
})
export class CadastrarClienteComponent {
  nome: string = "";
  endereco: string = "";
  telefone: string = "";


  constructor(
    private client: HttpClient,
    private router: Router,
    private snackBar: MatSnackBar
  ) {}


  cadastrar(): void {
    let cliente: Cliente = {
      nome: this.nome,
      endereco: this.endereco,
      telefone: Number.parseFloat(this.telefone),
    };

    this.client
      .post<Cliente>("https://localhost:7288/api/cliente/cadastrar", cliente)
      .subscribe({
        //A requição funcionou
        next: (cliente) => {
          this.snackBar.open("Cliente cadastrado com sucesso!!", "OK", {
            duration: 2000,
            horizontalPosition: "left",
            verticalPosition: "top",
          });
          this.router.navigate(["pages/cliente/listar-cliente"]);
        },
        //A requição não funcionou
        error: (erro) => {
          console.log(erro);
        },
      });
  }
}
