import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { Cliente } from 'src/app/models/cliente.models';

@Component({
  selector: 'app-alterar-cliente',
  templateUrl: "./alterar-cliente.component.html" ,
  styleUrls: ["./alterar-cliente.component.css"],
})
export class AlterarClienteComponent {
  clienteId: number = 0;
  nome: string = "";
  endereco: string = "";
  telefone: number = 0;
  

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
        this.client.get<Cliente>(`https://localhost:7288/api/cliente/buscar/${id}`).subscribe({
          next: (cliente) => {
            this.clienteId = cliente.clienteId!;
            this.nome = cliente.nome;
            this.endereco = cliente.endereco;
            this.telefone = cliente.telefone;
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
    let cliente: Cliente = {
      nome: this.nome,
      endereco: this.endereco,
      telefone: this.telefone
    };

    console.log(cliente);

    this.client.put<Cliente>(`https://localhost:7288/api/cliente/alterar/${this.clienteId}`, cliente).subscribe({
      //A requição funcionou
      next: (cliente) => {
        this.snackBar.open("Cliente alterado com sucesso!!", "OK", {
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
