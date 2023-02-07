import {Time} from "@angular/common";
import {Team} from "./teams";

export class Schedule {
  id: number = 0;
  date?: Date;
  time?: Time;
  place: number = 0;
  round: number = 0;
  host: number = 0;
  guest: number = 0;
  hostWins: number = 1;
  guestWins: number = 1;
  hostNavigation!: Team;
  guestNavigation?: Team;
  placeNavigation ?: number;
  doubles?: [];
  singles?: [];
}
