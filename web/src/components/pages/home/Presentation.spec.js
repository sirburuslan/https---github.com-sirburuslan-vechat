// Installed Utils
import { mount } from '@vue/test-utils';
import { describe, expect, beforeAll, afterAll, test } from 'vitest';

// App Utils
import Presentation from '@/components/pages/home/Presentation.vue';

// Test the Presentation Component
describe('Presentation', () => {
  let wrapper;

  beforeAll(() => {
    wrapper = mount(Presentation);
  });

  afterAll(() => {
    wrapper.unmount();
  });

  test('is a Vue instance', () => {
    expect(wrapper.vm).toBeTruthy();
  });

  test('renders elements loading correctly', () => {
    expect(wrapper.find('.vc-presentation').exists()).toBeTruthy();
    expect(wrapper.find('.vc-presentation > .vc-presentation-container').exists()).toBeTruthy();
    expect(wrapper.find('.vc-presentation > .vc-presentation-container > .vc-presentation-text').exists()).toBeTruthy();
    expect(wrapper.find('.vc-presentation > .vc-presentation-container > .vc-presentation-text > h1').exists()).toBeTruthy();
    expect(wrapper.find('.vc-presentation > .vc-presentation-container > .vc-presentation-text > h4').exists()).toBeTruthy();
    expect(wrapper.find('.vc-presentation > .vc-presentation-container > .vc-presentation-text > .vc-book-call').exists()).toBeTruthy();
    expect(wrapper.find('.vc-presentation > .vc-presentation-container > .vc-presentation-text > .vc-get-started').exists()).toBeTruthy();
    expect(wrapper.find('.vc-presentation > .vc-presentation-container > .vc-presentation-text > h6').exists()).toBeTruthy();
    expect(wrapper.find('.vc-presentation > .vc-presentation-container > .vc-presentation-image').exists()).toBeTruthy();
  });

  test('render icons component correctly', () => {
    expect(wrapper.findAllComponents({ name: 'Icon' })).toHaveLength(2);
  });

  test('render image correctly', () => {
    const img = wrapper.find('.vc-presentation > .vc-presentation-container > .vc-presentation-image > img');
    expect(img.exists()).toBeTruthy();
    expect(img.attributes('src')).toBe('placeholder.png');
  });

  test('renders links loading correctly', () => {
    expect(wrapper.findAll('a')).toHaveLength(2);
  });
  
});
