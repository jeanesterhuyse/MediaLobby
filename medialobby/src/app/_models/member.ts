import { Photo } from "./photo";

export interface Member {
    id: number;
    userName: string;
    userEmail: string;
    photos: Photo[];
    photoUrl: string;
}


