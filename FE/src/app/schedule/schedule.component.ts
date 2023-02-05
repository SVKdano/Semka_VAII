import { Component } from '@angular/core';
import {TeamsServiceService} from "../services/teams-service.service";
import {Schedule} from "../models/schedule";
import {ActivatedRoute} from "@angular/router";

@Component({
  selector: 'app-schedule',
  templateUrl: './schedule.component.html',
  styleUrls: ['./schedule.component.css']
})
export class ScheduleComponent {
  schedule: Schedule[] =[];

  constructor(private teamService: TeamsServiceService, private route: ActivatedRoute) {}

  ngOnInit() : void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.teamService.getSchedule(id).subscribe((result: Schedule[]) => (this.schedule = result));
  }
}
