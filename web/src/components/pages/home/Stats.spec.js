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
import Stats from '@/components/pages/home/Stats.vue';

// Test the Stats Component
describe('Stats', () => {
  let wrapper;

  beforeAll(() => {
    wrapper = mount(Stats);
  });

  afterAll(() => {
    wrapper.unmount();
  });

  test('is a Vue instance', () => {
    expect(wrapper.vm).toBeTruthy();
  });

  test('renders elements loading correctly', () => {
    expect(wrapper.find('.vc-stats').exists()).toBeTruthy();
    expect(wrapper.find('.vc-stats > .vc-stats-container').exists()).toBeTruthy();
    expect(wrapper.findAll('.vc-stats > .vc-stats-container > .vc-stat')).toHaveLength(4);
    expect(wrapper.find('.vc-stats > .vc-stats-container > .vc-stat > a').exists()).toBeTruthy();
    expect(wrapper.findAll('.vc-stats > .vc-stats-container .vc-stat > h3')).toHaveLength(3);
    expect(wrapper.findAll('.vc-stats > .vc-stats-container .vc-stat > h6')).toHaveLength(3);
  });

  test('render icons component correctly', () => {
    expect(wrapper.findAllComponents({ name: 'Icon' }).length).toBe(1);
  });

  test('renders links loading correctly', () => {
    expect(wrapper.findAll('a').length).toBe(1);
  });
});
