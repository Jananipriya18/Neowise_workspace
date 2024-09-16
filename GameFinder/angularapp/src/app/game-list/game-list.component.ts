import { Component, OnInit } from '@angular/core';
import { Game } from '../model/game.model';
import { GameService } from '../services/game.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-game-list',
  templateUrl: './game-list.component.html',
  styleUrls: ['./game-list.component.css'],
})
export class GameListComponent implements OnInit {
  games: Game[] = [];

  constructor(private gameService: GameService, private router: Router) {}

  ngOnInit(): void {
    this.getGames();
  }

  getGames(): void {
    try {
      this.gameService.getGames().subscribe(
        (res) => {
          console.log(res);
          this.games = res;
        },
        (err) => {
          console.log(err);
        }
      );
    } catch (err) {
      console.log('Error:', err);
    }
  }

  editGame(id: number): void {
    this.router.navigate(['/edit', id]);
  }

  deleteGame(id: any): void {
    this.gameService.deleteGame(id).subscribe(() => {
      this.games = this.games.filter((game) => game.id !== id);
    });
  }
}
