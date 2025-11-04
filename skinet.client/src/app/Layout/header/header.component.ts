import { Component, OnInit } from '@angular/core';
import { MatIcon } from '@angular/material/icon';
import { MatButton } from '@angular/material/button';
import { MatBadge } from '@angular/material/badge'
import { RouterLink, RouterLinkActive } from '@angular/router';

@Component({
  imports: [
    MatIcon,
    MatButton,
    MatBadge, RouterLink, RouterLinkActive
  ],
  selector: 'app-header',
  standalone: true,
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent  {



}
