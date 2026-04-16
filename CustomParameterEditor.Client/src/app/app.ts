import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavMenu } from './navmenu/nav-menu';

@Component({
  selector: 'app-root',
  standalone: true,
	imports: [RouterOutlet, NavMenu],

  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
	protected readonly title = signal('CustomParameterEditor');
}
