export class Tarefa {
    constructor (descricao: string, finalizada: boolean) {
        this.Descricao = descricao;
        this.Finalizada = finalizada;
    }

    Descricao?: string;
    Finalizada?: boolean;
}