import {Team} from "./teams";

export class Place {
  id: number = 0;
  teamId: number = 0;
  address: string = "";
  team!: Team;
}
