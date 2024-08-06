import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api/menuitem';

@Component({
  selector: 'app-database-users',
  templateUrl: './database-users.component.html',
  styleUrls: ['./database-users.component.css']
})
export class DatabaseUsersComponent implements OnInit {

  public items: MenuItem[];
  public home: MenuItem;

  constructor() { }

  ngOnInit() {
  }

}
