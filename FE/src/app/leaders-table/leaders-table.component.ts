import { Component } from '@angular/core';

export interface Leaders {
  name: string;
  league: number;
  points: number;
}

const LEADERS_TABLE: Leaders[] = [
  {name: 'TTC PB B', league: 5, points: 50 },
  {name: 'Sedmerovec', league: 6, points: 53 },
  {name: 'Nova Dubnica', league: 7, points: 56 },
];


@Component({
  selector: 'app-leaders-table',
  templateUrl: './leaders-table.component.html',
  styleUrls: ['./leaders-table.component.css']
})
export class LeadersTableComponent {
  displayedColumns: string[] = ['league', 'name', 'points'];
  dataSource = LEADERS_TABLE;
  clicked = new Set<string>();
}
