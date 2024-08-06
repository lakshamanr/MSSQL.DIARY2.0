import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api/menuitem';

@Component({
  selector: 'app-database-user-details',
  templateUrl: './database-user-details.component.html',
  styleUrls: ['./database-user-details.component.css']
})
export class DatabaseUserDetailsComponent implements OnInit {

  public items: MenuItem[];
  public home: MenuItem;

  constructor() { }

  ngOnInit() {
  }

}
