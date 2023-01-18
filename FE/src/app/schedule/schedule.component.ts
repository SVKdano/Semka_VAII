import { Component } from '@angular/core';
import {TeamsServiceService} from "../services/teams-service.service";
import {Schedule} from "../models/schedule";

@Component({
  selector: 'app-schedule',
  templateUrl: './schedule.component.html',
  styleUrls: ['./schedule.component.css']
})
export class ScheduleComponent {
  schedule: Schedule[] =[];

  constructor(private teamService: TeamsServiceService) {}

  ngOnInit() : void {
    this.teamService.getSchedule(5).subscribe((result: Schedule[]) => (this.schedule = result));
  }



}
