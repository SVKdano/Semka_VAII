import { Component } from '@angular/core';
import {Place} from "../models/place";
import {TeamsServiceService} from "../services/teams-service.service";
import {ActivatedRoute} from "@angular/router";

@Component({
  selector: 'app-place',
  templateUrl: './place.component.html',
  styleUrls: ['./place.component.css']
})
export class PlaceComponent {
  place: Place[] = [];

  constructor(private teamService: TeamsServiceService,
              private route: ActivatedRoute) {
  }

  ngOnInit() : void {
    const placeId = Number(this.route.snapshot.paramMap.get('placeId'));
    this.teamService.getPlaces(placeId).subscribe((result: Place[]) => (this.place = result));
  }
}
