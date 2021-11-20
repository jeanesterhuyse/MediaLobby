import { Photo } from "./photo";
import {Folder} from"./folder";

export interface Member {
    id: number;
    userName: string;
    userEmail: string;
    folders: Folder[];
    photos: Photo[];
    photoUrl: string;
}


