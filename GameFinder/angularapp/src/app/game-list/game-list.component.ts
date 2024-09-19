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
  developerName: string = '';
  developerGameCount: number = 0;

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

  // Method to search games by developer
  searchByDeveloper(): void {
    if (this.developerName.trim() !== '') {
      this.gameService.getGamesByDeveloper(this.developerName).subscribe(
        (res) => {
          this.games = res;
          this.developerGameCount = res.length; // Set the count of games
        },
        (err) => {
          console.log(err);
        }
      );
    } else {
      // Reset to all games if no developer is entered
      this.getGames();
      this.developerGameCount = 0;
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
