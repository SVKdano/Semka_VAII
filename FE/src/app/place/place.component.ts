import { Component } from '@angular/core';
import {Team} from "../models/teams";
import {Place} from "../models/place";
import {TeamsServiceService} from "../services/teams-service.service";
import {ActivatedRoute} from "@angular/router";

@Component({
  selector: 'app-place',
  templateUrl: './place.component.html',
  styleUrls: ['./place.component.css']
})
export class PlaceComponent {
  teams: Team[] = [];
  place: Place[] = [];

  constructor(private teamService: TeamsServiceService,
              private route: ActivatedRoute) {
  }

  ngOnInit() : void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    const placeId = Number(this.route.snapshot.paramMap.get('placeId'));
    this.teamService.getTeams(id).subscribe((result: Team[]) => (this.teams = result));
    this.teamService.getPlaces(placeId).subscribe((result: Place[]) => (this.place = result));
    setTimeout(() => { this.ngOnInit() }, 1000);
  }
}
