import {Component, OnInit} from '@angular/core';
import {TeamsServiceService} from "../services/teams-service.service";
import {Schedule} from "../models/schedule";
import {ActivatedRoute} from "@angular/router";

@Component({
  selector: 'app-schedule',
  templateUrl: './schedule.component.html',
  styleUrls: ['./schedule.component.css']
})
export class ScheduleComponent implements OnInit {
  schedule: Schedule[] = [];
  value: number = 0;
  rounds: number = 0;

  constructor(private teamService: TeamsServiceService, private route: ActivatedRoute) {}

  ngOnInit() : void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.teamService.getLeagueRounds(id).subscribe((result: number) => (this.rounds = result));
    this.teamService.getSchedule(id).subscribe((result: Schedule[]) => (this.schedule = result));
  }

  setValue(round: number) : void {
    this.value = round;
  }

  changedRound() : void {
    this.schedule = [];
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.teamService.getScheduleOfRound(id, this.value).subscribe((result: Schedule[]) => (this.schedule = result));
  }
}
