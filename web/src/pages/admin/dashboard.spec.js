// Installed Utils
import { mount, config } from '@vue/test-utils';
import {
    describe,
    expect,
    beforeAll,
    afterAll,
    test,
} from 'vitest';

// App Utils
import dashboard from '@/pages/admin/dashboard.vue';
import Dropdown from '@/components/ui/dropdown.vue';

// Register the Dropdown component for global usage
config.global.components = {
    Dropdown
}

// Test the Dashboard Page
describe('Dashboard Page', () => {
    let wrapper;

    beforeAll(() => {
        wrapper = mount(dashboard);
    });

    afterAll(() => {
        wrapper.unmount();
    });

    test('is a Vue instance', () => {
        expect(wrapper.vm).toBeTruthy();
    });

    test('renders elements loading correctly', () => {
      expect(wrapper.find('.vc-dashboard-container').exists()).toBe(true);
      expect(
        wrapper.find('.vc-dashboard-container > .vc-events-section').exists(),
      ).toBe(true);
      expect(
        wrapper.findAll('.vc-dashboard-container > .vc-events-section > .flex'),
      ).toHaveLength(2);
      expect(
        wrapper
          .find(
            '.vc-dashboard-container > .vc-events-section > .flex > .w-full',
          )
          .exists(),
      ).toBe(true);
      expect(
        wrapper
          .find(
            '.vc-dashboard-container > .vc-events-section > .flex > .w-full > .vc-page-title',
          )
          .exists(),
      ).toBe(true);
      expect(
        wrapper
          .find(
            '.vc-dashboard-container > .vc-events-section > .flex > .vc-events',
          )
          .exists(),
      ).toBe(true);
      expect(
        wrapper
          .find(
            '.vc-dashboard-container > .vc-events-section > .flex > .vc-events > .vc-events-header',
          )
          .exists(),
      ).toBe(true);
      expect(
        wrapper
          .find(
            '.vc-dashboard-container > .vc-events-section > .flex > .vc-events > .vc-events-header > .vc-events-date-navigator',
          )
          .exists(),
      ).toBe(true);
      expect(
        wrapper
          .find(
            '.vc-dashboard-container > .vc-events-section > .flex > .vc-events > .vc-events-header > .vc-events-date-navigator > .flex',
          )
          .exists(),
      ).toBe(true);
      expect(
        wrapper.findAll(
          '.vc-dashboard-container > .vc-events-section > .flex > .vc-events > .vc-events-header > .vc-events-date-navigator > .flex > .flex-none',
        ),
      ).toHaveLength(2);
    });

})