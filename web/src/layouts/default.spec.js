// Installed Utils
import { mount } from '@vue/test-utils';
import {
  describe,
  expect,
  beforeAll,
  afterAll,
  test,
} from 'vitest';

// App Utils
import {default as defaultLayout} from '@/layouts/default.vue';

// Test the Default Layout
describe('DefaultLayout', () => {
    const slotContent = 'This is a test slot content';
    let wrapper;

    beforeAll(() => {
        wrapper = mount(defaultLayout, {
            slots: {
                default: slotContent,
            }
        });
    });

    afterAll(() => {
        wrapper.unmount();
    });

    test('renders slot content', () => {
        expect(wrapper.text()).toContain(slotContent);
    });

    test('renders elements correctly', () => {
        expect(wrapper.find('.vc-main').exists()).toBeTruthy();
    });

});