import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {Router} from "@angular/router";
import {TeamsServiceService} from "../services/teams-service.service";
import {Place} from "../models/place";
import {Team} from "../models/teams";
import {PlaceUpdate} from "../models/PlaceUpdate";
import {FormControl, FormGroup, Validators} from "@angular/forms";

@Component({
  selector: 'app-place-update',
  templateUrl: './place-update.component.html',
  styleUrls: ['./place-update.component.css']
})
export class PlaceUpdateComponent implements OnInit {
  @Input() place: PlaceUpdate = { id:0, teamId:0, address:""};
  @Input() team: Team = { id:0, name:"", league:0 };
  placeId: number = 0;
  placeTeamId: number = 0;
  placeAddress: string = "";

  teamId: number = 0;
  teamName: string = "";
  teamLeague: number = 0;

  placeForm: FormGroup = new FormGroup(
    {
      placeTeam: new FormControl(null, [Validators.required]),
      placeAddress: new FormControl(null, [Validators.required])
    }
  )

  places: Place[] = [];
  @Output() update = new EventEmitter<Place[]>();
  @Output() updatePlace = new EventEmitter<PlaceUpdate[]>();

  constructor(private router:Router, private service:TeamsServiceService) {
  }

  ngOnInit() {
  }

  fetchData() {
    this.service.getPlacesWithTeam().subscribe((result : Place[]) => (this.places = result));
    setTimeout(() => { this.fetchData() }, 2000);
  }

  initializeUpdating(id: number, teamId: number, address: string) {
    this.placeId = id;
    this.placeAddress = address;
    this.placeTeamId = teamId;
  }

  deletePlace(id: number) {
    this.service.deletePlace(id).subscribe(
      (place: Place[]) =>
      {
        this.update.emit(place);
        this.fetchData();
      }
    );
  }

  createPlace() {
    this.place.id = 0;
    this.place.teamId = this.placeTeamId;
    this.place.address = this.placeAddress;
    this.service.createPlace(this.place).subscribe( {
      next:((result:PlaceUpdate[]) => {
        this.updatePlace.emit(result);
        this.fetchData();
      }),
      error:(err =>
      {
        alert(err.error.message);
      })
    });
  }

  updatePlaceM() {
    this.place.id = this.placeId;
    this.place.address = this.placeAddress;
    this.place.teamId = this.placeTeamId;

    this.service.updatePlace(this.place).subscribe( {
      next:((result:PlaceUpdate[]) =>
      {
        this.updatePlace.emit(result);
        this.fetchData();
      }),
      error:(err =>
      {
        alert(err.error.message);
      })
    });
  }

  public logout() {
    localStorage.clear();
    this.router.navigate(["/"]);
  }
}
