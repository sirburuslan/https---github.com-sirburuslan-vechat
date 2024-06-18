// Installed Utils
import { createPinia, setActivePinia } from 'pinia';
import { describe, vi, test, expect, beforeEach } from 'vitest';

// App Utils
import type { User } from '~/interfaces/user';

describe('User Store', () => {
  const userStore = useUserStore();

  beforeEach(() => {
    setActivePinia(createPinia());
    userStore.$reset();
  });

  test('it should initialize the state correctly', () => {
    expect(userStore.user).toBeNull();
    expect(userStore.token).toBeNull();
    expect(userStore.isAuthenticated).toBeFalsy();
    expect(userStore.isLoading).toBeFalsy();
  });

  test('it should save the data correctly', () => {
    const userData: User = {
      userId: 1,
      firstName: 'John',
      lastName: 'Doe',
      email: 'john.doe@example.com',
      role: 1,
      sidebarStatus: 1,
      token: 'token123',
    };

    userStore.saveUser(userData);

    expect(userStore.user).toEqual({
      userId: '1',
      firstName: 'John',
      lastName: 'Doe',
      email: 'john.doe@example.com',
      role: 1,
      sidebarStatus: 1,
    });

    expect(userStore.token).toBe('token123');
    expect(userStore.isAuthenticated).toBeTruthy();
    expect(userStore.isLoading).toBeFalsy();
  });

  test('should update sidebar status correctly', () => {
    userStore.saveUser({
      userId: 1,
      firstName: 'John',
      lastName: 'Doe',
      email: 'john.doe@example.com',
      role: 1,
      sidebarStatus: 1,
      token: 'token123',
    });

    expect(userStore.sidebarStatus).toBe(1);

    userStore.updateSidebarStatus(1);
    expect(userStore.sidebarStatus).toBe(1);

    userStore.$reset();
    userStore.updateSidebarStatus(2);
    expect(userStore.user?.sidebarStatus).toBe(2);
    expect(userStore.isAuthenticated).toBeFalsy();
    expect(userStore.isLoading).toBeFalsy();
  });
});
