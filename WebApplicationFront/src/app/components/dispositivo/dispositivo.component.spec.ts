
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { TestBed } from '@angular/core/testing';

import { DispositivoComponent } from './dispositivo.component';

describe('DispositivoComponent', () => {
  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DispositivoComponent],
    }).compileComponents();
  });
});

