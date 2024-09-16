// edit-game.component.ts
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Game } from '../model/game.model';
import { GameService } from '../services/game.service';

@Component({
  selector: 'app-edit-game',
  templateUrl: './edit-game.component.html',
  styleUrls: ['./edit-game.component.css']
})
export class EditGameComponent implements OnInit {
  game: Game | null = null;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private gameService: GameService
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.gameService.getGameById(+id).subscribe(
        (game) => (this.game = game),
        (err) => console.error(err)
      );
    }
  }

  saveGame(): void {
    if (this.game) {
      this.gameService.updateGame(this.game.id, this.game).subscribe(
        () => {
          this.router.navigate(['/games']);
        },
        (err) => {
          console.error(err);
        }
      );
    }
  }

  cancel(): void {
    this.router.navigate(['/games']);
  }
}
