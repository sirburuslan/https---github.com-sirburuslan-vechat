// Installed Utils
import {
    describe,
    test,
    vi,
    expect,
    afterEach
} from 'vitest';

describe('showNotification', () => {

  const showNotification = vi.fn((type: string, message: string) => {
    // Set class for colors
    const bgColor = type === 'success' ? 'vc-popup-notification-success' : 'vc-popup-notification-error';

    // Create popup
    const popup = `<div class="vc-popup-notification ${bgColor}">${message}</div>`;

    // Insert popup
    document.body.insertAdjacentHTML('beforeend', popup);

    // Wait 2.5 seconds and start to hide the message
    setTimeout(() => {
      (document.getElementsByClassName('vc-popup-notification')[0] as HTMLElement).style.opacity = '0';
    }, 2500);

    // Remove the message
    setTimeout(() => {
      document.getElementsByClassName('vc-popup-notification')[0].remove();
    }, 3000);
  });

  afterEach(() => {
    document.body.innerHTML = '';
    vi.restoreAllMocks();
  });

  test('should show a success notification', () => {
    showNotification('success', 'Operation successful');
    const notification = document.querySelector('.vc-popup-notification');
    expect(notification).not.toBeNull();
    expect(notification?.classList).toContain('vc-popup-notification-success');
    expect(notification?.textContent).toBe('Operation successful');
  });

  test('should show an error notification', () => {
    showNotification('error', 'Something went wrong');

    const notification = document.querySelector('.vc-popup-notification');
    expect(notification).not.toBeNull();
    expect(notification?.classList).toContain('vc-popup-notification-error');
    expect(notification?.textContent).toBe('Something went wrong');
  });

  test('should remove the notification after 3 seconds', async () => {
    vi.useFakeTimers();
    showNotification('success', 'Operation successful');

    let notification = document.querySelector('.vc-popup-notification');
    expect(notification).not.toBeNull();

    if ( notification instanceof HTMLElement ) {

        vi.advanceTimersByTime(2500);
        expect(notification?.style.opacity).toBe('0');

    }

    vi.advanceTimersByTime(500);
    notification = document.querySelector('.vc-popup-notification');
    expect(notification).toBeNull();

    vi.useRealTimers();
    
  });

});