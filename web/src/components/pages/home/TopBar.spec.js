// Installed Utils
import { mount } from '@vue/test-utils';
import { describe, expect, beforeAll, afterAll, test } from 'vitest';

// App Utils
import TopBar from '@/components/pages/home/TopBar.vue';

// Test the TopBar Component
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

  test('renders elements loading correctly', () => {
    expect(wrapper.find('.vc-top-bar').exists()).toBeTruthy();
    expect(wrapper.find('.vc-top-bar > .vc-top-bar-container').exists()).toBeTruthy();
    expect(wrapper.find('.vc-top-bar > .vc-top-bar-container > .vc-top-bar-logo').exists()).toBeTruthy();
    expect(wrapper.find('.vc-top-bar > .vc-top-bar-container > .vc-top-bar-logo > a').exists()).toBeTruthy();    
    expect(wrapper.find('.vc-top-bar > .vc-top-bar-container > .vc-top-bar-menu').exists()).toBeTruthy();
    expect(wrapper.find('.vc-top-bar > .vc-top-bar-container > .vc-top-bar-auth').exists()).toBeTruthy();
    expect(wrapper.find('.vc-top-bar > .vc-top-bar-container > .vc-top-bar-auth > .vc-auth-sign-in').exists()).toBeTruthy();
    expect(wrapper.find('.vc-top-bar > .vc-top-bar-container > .vc-top-bar-auth > .vc-auth-sign-up').exists()).toBeTruthy();
  });

  test('renders links loading correctly', () => {
    expect(wrapper.findAll('a')).toHaveLength(6);
  });
});
