import { Photo } from './photo';

export interface User {
    id: number;
    nome: string;
    conhecidoComo: string;
    idade: number;
    genero: string;
    criado: Date;
    ultimaAtividade: Date;
    photoUrl: string;
    cidade: string;
    pais: string;
    interesses?: string;
    introducao?: string;
    procurandoPor?: string;
    photos?: Photo[];
}
