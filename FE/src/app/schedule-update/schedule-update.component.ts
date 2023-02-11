import {Component, EventEmitter, Output} from '@angular/core';
import {Schedule} from "../models/schedule";
import {Router} from "@angular/router";
import {TeamsServiceService} from "../services/teams-service.service";
import {Place} from "../models/place";

@Component({
  selector: 'app-schedule-update',
  templateUrl: './schedule-update.component.html',
  styleUrls: ['./schedule-update.component.css']
})
export class ScheduleUpdateComponent {
  schedule: Schedule[] = [];
  @Output() update = new EventEmitter<Schedule[]>();

  constructor(private router:Router, private service:TeamsServiceService) {
  }

  fetchData() {
    this.service.getFullSchedule().subscribe((result:Schedule[]) => (this.schedule=result));
  }

  deleteEncounter(id: number) {
    this.service.deleteEncounter(id).subscribe(
      (schedule: Schedule[]) =>
      {
        this.update.emit(schedule);
        this.fetchData();
      }
    );
  }

  public logout() {
    localStorage.clear();
    this.router.navigate(["/"]);
  }
}
