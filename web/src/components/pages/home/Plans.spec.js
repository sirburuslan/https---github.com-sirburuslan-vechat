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
import Plans from '@/components/pages/home/Plans.vue';

// Test the Plans Component
describe('Plans', () => {
  let wrapper;

  beforeAll(() => {
    wrapper = mount(Plans);
  });

  afterAll(() => {
    wrapper.unmount();
  });

  test('is a Vue instance', () => {
    expect(wrapper.vm).toBeTruthy();
  });

  test('renders elements loading correctly', () => {
    expect(wrapper.find('.vc-plans').exists()).toBeTruthy();
    expect(wrapper.find('.vc-plans > .vc-plans-container').exists()).toBeTruthy();
    expect(wrapper.findAll('.vc-plans > .vc-plans-container > .w-full')).toHaveLength(2);
    expect(wrapper.find('.vc-plans > .vc-plans-container > .w-full > h2').exists()).toBeTruthy();
    expect(wrapper.find('.vc-plans > .vc-plans-container > .w-full > .flex').exists()).toBeTruthy();
  });
});
