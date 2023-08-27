import { AuthGuard } from './auth-guard.service';
import { Router } from '@angular/router';

describe('AuthGuard', () => {
  let authGuard: AuthGuard;
  let router: Router;

  beforeEach(() => {
    router = { navigate: jest.fn() } as any;
    authGuard = new AuthGuard(router);
  });

  it('should return true if the user is logged in', () => {
    localStorage.setItem('currentUser', JSON.stringify({}));
    expect(authGuard.canActivate()).toBe(true);
  });

  it('should return false and navigate to the login page if the user is not logged in', () => {
    localStorage.removeItem('currentUser');
    expect(authGuard.canActivate()).toBe(false);
    expect(router.navigate).toHaveBeenCalledWith(['/login']);
  });
});
