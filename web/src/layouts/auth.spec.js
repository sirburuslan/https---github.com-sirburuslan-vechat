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
import auth from '@/layouts/auth.vue';

// Test the Auth Layout
describe('AuthLayout', () => {
    const slotContent = 'This is a test slot content';
    let wrapper;

    beforeAll(() => {
        wrapper = mount(auth, {
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
        expect(wrapper.find('.vc-auth-main').exists()).toBeTruthy();
        expect(wrapper.find('.container').exists()).toBeTruthy();
        expect(wrapper.find('.mx-auto').exists()).toBeTruthy();
        expect(wrapper.find('.px-4').exists()).toBeTruthy();
        expect(wrapper.find('[class*="md:px-6"]').exists()).toBeTruthy();
        expect(wrapper.find('.vc-auth-container').exists()).toBeTruthy();
        expect(wrapper.find('.max-w-md').exists()).toBeTruthy();
        expect(wrapper.find('.mx-auto').exists()).toBeTruthy();
        expect(wrapper.find('.grid').exists()).toBeTruthy();
        expect(wrapper.find('.col-1').exists()).toBeTruthy();
    });
    
});