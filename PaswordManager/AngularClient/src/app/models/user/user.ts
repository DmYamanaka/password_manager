import { Guid } from 'guid-ts';
import { Card } from '../card';
import { UserPasswordHistory } from '../userPasswordHistory';

export interface User{
    id: Guid,
    name: string,
    photo?: string,
    hash: string,
    salt:string,
    createDate: Date,
    editDate?: Date,
    deleteDate?: Date,
    eMail?:string,
    phone?:string,
    cards:Card[],
    userPasswordHistory:UserPasswordHistory[];
}