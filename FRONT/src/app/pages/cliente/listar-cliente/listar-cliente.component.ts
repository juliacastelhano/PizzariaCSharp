import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Cliente } from 'src/app/models/cliente.models';

@Component({
  selector: 'app-listar-cliente',
  templateUrl: "./listar-cliente.component.html",
  styleUrls: ["./listar-cliente.component.css"],
})
export class ListarClienteComponent {
  colunasTabela: string[] = [
    "id",
    "nome",
    "endereco",
    "telefone",
    "deletar",
    // "alterar",
  ];

  clientes: Cliente[] = [];

  constructor(private client: HttpClient, private snackBar: MatSnackBar) {}

  //Método que é executado ao abrir um componente
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
  }

  deletar(clienteId: number) {
    this.client
      .delete<Cliente[]>(
        `https://localhost:7288/api/cliente/deletar/${clienteId}`
      )
      .subscribe({
        //Requisição com sucesso
        next: (clientes) => {
          this.clientes = clientes;
          this.snackBar.open("Cliente deletado com sucesso!!", "OK", {
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
}
