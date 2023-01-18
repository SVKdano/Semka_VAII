import {Time} from "@angular/common";
import {Team} from "./teams";

export class Schedule {
  id: number = 0;
  date?: Date;
  time?: Time;
  place: number = 0;
  host: number = 0;
  guest: number = 0;
  hostWins?: number;
  guestWins?: number;
  hostNavigation!: Team;
  guestNavigation!: Team;
  placeNavigation ?: number;
  doubles?: [];
  singles?: [];
}
