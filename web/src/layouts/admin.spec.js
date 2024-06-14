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
import admin from '@/layouts/admin.vue';

// Test the Admin Layout
describe('Admin Layout', () => {
    let wrapper;

    // I'm not creating in this case a mock, the store's method is just changing the property value
    const store = useUserStore();
    store.updateSidebarStatus(1);

    beforeAll(() => {
        wrapper = mount(admin);
    });

    afterAll(() => {
        wrapper.unmount();
    });

    test('is a Vue instance', () => {
        expect(wrapper.vm).toBeTruthy();
    });
    
    test('is sidebar minimizing', async () => {
        expect(wrapper.find('.vc-minimized-sidebar').exists()).toBe(true);
    });  

});