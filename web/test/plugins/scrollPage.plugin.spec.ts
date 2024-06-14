// Installed Utils
import {
  describe,
  test,
  vi,
  expect,
  afterEach
} from 'vitest';

// App Utils
import { scrollPageDirective } from '~/directives/scrollPage.directive';

describe('scrollPageDirective', () => {
  const scrollToMock = vi.spyOn(window, 'scrollTo');

  afterEach(() => {
    scrollToMock.mockReset();
  });

  test('should scroll to the features section', () => {
    const el = {
      addEventListener: vi.fn(),
      getAttribute: vi.fn().mockReturnValue('#features'),
    };
    const featuresEl = document.createElement('div');
    featuresEl.classList.add('vc-features');
    document.body.appendChild(featuresEl);

    scrollPageDirective.mounted(el);

    const eventHandler = el.addEventListener.mock.calls[0][1];
    const event = new MouseEvent('click', { bubbles: true, cancelable: true });
    eventHandler(event);

    expect(scrollToMock).toHaveBeenCalledWith({
      top: featuresEl.offsetTop,
      behavior: 'smooth',
    });
  });

  test('should scroll to the pricing section', () => {
    const el = {
      addEventListener: vi.fn(),
      getAttribute: vi.fn().mockReturnValue('#pricing'),
    };
    const plansEl = document.createElement('div');
    plansEl.classList.add('vc-plans');
    document.body.appendChild(plansEl);

    scrollPageDirective.mounted(el);

    const eventHandler = el.addEventListener.mock.calls[0][1];
    const event = new MouseEvent('click', { bubbles: true, cancelable: true });
    eventHandler(event);

    expect(scrollToMock).toHaveBeenCalledWith({
      top: plansEl.offsetTop,
      behavior: 'smooth',
    });
  });

  test('should scroll to the faq section', () => {
    const el = {
      addEventListener: vi.fn(),
      getAttribute: vi.fn().mockReturnValue('#faq'),
    };
    const faqEl = document.createElement('div');
    faqEl.classList.add('vc-faq');
    document.body.appendChild(faqEl);

    scrollPageDirective.mounted(el);

    const eventHandler = el.addEventListener.mock.calls[0][1];
    const event = new MouseEvent('click', { bubbles: true, cancelable: true });
    eventHandler(event);

    expect(scrollToMock).toHaveBeenCalledWith({
      top: faqEl.offsetTop,
      behavior: 'smooth',
    });
  });
});