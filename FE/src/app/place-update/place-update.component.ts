import {Component, EventEmitter, Injectable, Input, OnInit, Output} from '@angular/core';
import {Router} from "@angular/router";
import {TeamsServiceService} from "../services/teams-service.service";
import {Place} from "../models/place";
import {Player} from "../models/player";

@Component({
  selector: 'app-place-update',
  templateUrl: './place-update.component.html',
  styleUrls: ['./place-update.component.css']
})
export class PlaceUpdateComponent implements OnInit {
  places: Place[] = [];
  placeId: number = 0;
  @Output() update = new EventEmitter<Place[]>();

  constructor(private router:Router, private service:TeamsServiceService) {
  }

  ngOnInit() {
    this.service.getAllPlaces().subscribe((result : Place[]) => (this.places = result));
  }

  deletePlace(id: number) {
    this.service.deletePlace(this.placeId).subscribe( (place: Place[]) => this.update.emit(place));
  }

  public logout() {
    localStorage.clear();
    this.router.navigate(["/"]);
  }
}
