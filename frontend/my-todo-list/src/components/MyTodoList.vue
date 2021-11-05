<template>
  <v-container>
    <h1>My TODO List</h1>

    <v-form>
      <v-row>
        <v-col cols="4" sm="6" md="3">
          <v-text-field
            v-model="descricaoTarefa"
            label="Descrição"
            required
          ></v-text-field>
        </v-col>

        <v-col cols="4" sm="6" md="3" class="pt-6">
          <v-btn block depressed color="primary" @click="adicionarTarefa()">
            Cadastrar
          </v-btn>
        </v-col>
      </v-row>
    </v-form>

    <form>
      <v-simple-table>
        <template v-slot:default>
          <thead>
            <tr>
              <th class="text-left"></th>
              <th class="text-left">Tarefa</th>
              <th class="text-left">Ações</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="(tarefa, index) in tarefas" :key="index">
              <td>
                <v-checkbox
                  v-model="tarefasSelecionadas" :value="tarefa.id"
                ></v-checkbox>
              </td>
              <td v-if="tarefa.finalizada" class="green lighten-4">
                {{ tarefa.descricao }}
              </td>
              <td v-else>{{ tarefa.descricao }}</td>
              <td>
                <v-row>
                  <v-dialog width="500">
                    <template v-slot:activator="{ on, attrs }">
                      <v-col cols="4" sm="6" md="3" v-bind="attrs" v-on="on">
                        <v-btn block depressed small> Editar </v-btn>
                      </v-col>
                    </template>

                    <v-card>
                      <v-card-title class="text-h5 grey lighten-2">
                        Editar Tarefa
                      </v-card-title>

                      <v-card-text>
                        <v-col>
                          <v-text-field
                            v-model="descricaoAlteradaTarefa"
                            label="Descrição"
                            required
                          ></v-text-field>
                        </v-col>
                      </v-card-text>

                      <v-divider></v-divider>

                      <v-card-actions>
                        <v-spacer></v-spacer>
                        <v-btn
                          color="primary"
                          text
                          @click="editarTarefa(tarefa), (dialog = false)"
                        >
                          Salvar
                        </v-btn>
                      </v-card-actions>
                    </v-card>
                  </v-dialog>

                  <v-col cols="4" sm="6" md="3">
                    <v-btn
                      block
                      depressed
                      small
                      color="error"
                      @click="excluirTarefa(tarefa.id)"
                    >
                      Excluir
                    </v-btn>
                  </v-col>

                  <v-col
                    v-if="!tarefa.finalizada"
                    cols="4"
                    sm="6"
                    md="3"
                    @click="finalizarTarefas([tarefa.id])"
                  >
                    <v-btn block depressed small color="success">
                      Finalizar
                    </v-btn>
                  </v-col>
                </v-row>
              </td>
            </tr>
          </tbody>
        </template>
      </v-simple-table>

      <br />

      <v-btn class="mr-4" @click="selecionarTodasTarefas()"> Selecionar Todos </v-btn>
      <v-btn @click="finalizarTarefasSelecionados()"> Finalizar Selecionados </v-btn>
    </form>
  </v-container>
</template>

<script>
import { Tarefa } from "@/models/Tarefa";

export default {
  name: "Home",

  data: () => ({
    descricaoTarefa: "",
    descricaoAlteradaTarefa: "",
    tarefas: [],
    tarefasSelecionadas: [],
  }),

  created() {
    this.consultarTarefas();
  },

  methods: {
    consultarTarefas() {
      this.$http
        .get("http://localhost:12086/tarefa")
        .then((res) => res.data)
        .then((tarefas) => {
          this.tarefas = tarefas;
        });
    },

    adicionarTarefa() {
      let tarefa = new Tarefa(this.descricaoTarefa, false);

      this.$http
        .post("http://localhost:12086/tarefa", tarefa)
        .then((res) => res.data)
        .then(() => {
          this.consultarTarefas();
        });
    },

    editarTarefa(tarefa) {
      tarefa.descricao = this.descricaoAlteradaTarefa;

      this.$http
        .post("http://localhost:12086/tarefa", tarefa)
        .then((res) => res.data)
        .then(() => {
          this.consultarTarefas();
          this.descricaoAlteradaTarefa = "";
        });
    },

    finalizarTarefas(ids) {
      this.$http.put("http://localhost:12086/tarefa", ids).then(() => {
        this.consultarTarefas();
      });
    },

    excluirTarefa(id) {
      this.$http.delete(`http://localhost:12086/tarefa?id=${id}`).then(() => {
        this.consultarTarefas();
      });
    },

    selecionarTodasTarefas(){
      this.tarefasSelecionadas = this.tarefas.map(t => t.id);
    },

    finalizarTarefasSelecionados(){
      this.finalizarTarefas(this.tarefasSelecionadas);
      this.tarefasSelecionadas = [];
    }
  },
};
</script>
