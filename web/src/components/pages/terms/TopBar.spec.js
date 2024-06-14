// Installed Utils
import { mount } from '@vue/test-utils';
import {
  describe,
  expect,
  beforeAll,
  afterAll,
  test
} from 'vitest';

// App Utils
import TopBar from './TopBar.vue';

// Test the TopBar component
describe('TopBar', () => {
    let wrapper;

    beforeAll(() => {
        wrapper = mount(TopBar);
    });

    afterAll(() => {
        wrapper.unmount();
    });

    test('is a Vue instance', () => {
        expect(wrapper.vm).toBeTruthy();
    });



});