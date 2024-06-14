// Installed Utils
import { mount } from '@vue/test-utils';
import { describe, expect, beforeAll, afterAll, test } from 'vitest';

// App Utils
import Faq from '@/components/pages/home/Faq.vue';

// Test the Faq Component
describe('Faq', () => {
  let wrapper;

  beforeAll(() => {
    wrapper = mount(Faq);
  });

  afterAll(() => {
    wrapper.unmount();
  });

  test('is a Vue instance', () => {
    expect(wrapper.vm).toBeTruthy();
  });

  test('renders loading elements correctly', () => {
    expect(wrapper.find('.vc-faq').exists()).toBeTruthy();
    expect(wrapper.find('.vc-faq > .vc-faq-container').exists()).toBeTruthy();
    expect(wrapper.findAll('.vc-faq > .vc-faq-container > .w-full')).toHaveLength(2);
    expect(wrapper.find('.vc-faq > .vc-faq-container > .w-full > h2').exists()).toBeTruthy();
    expect(wrapper.find('.vc-faq > .vc-faq-container > .w-full > ul').exists()).toBeTruthy();
    expect(wrapper.findAll('.vc-faq > .vc-faq-container > .w-full > ul > li')).toHaveLength(7);
    expect(wrapper.findAll('.vc-faq > .vc-faq-container > .w-full > ul > li > a')).toHaveLength(7);
    expect(wrapper.findAll('.vc-faq > .vc-faq-container > .w-full > ul > li > a > span')).toHaveLength(14);
    expect(wrapper.findAll('.vc-faq > .vc-faq-container > .w-full > ul > li > .vc-faq-answer')).toHaveLength(7);
    expect(wrapper.findAll('.vc-faq > .vc-faq-container > .w-full > ul > li > .vc-faq-answer > p')).toHaveLength(7);
  });

  test('renders icon component', () => {
    expect(wrapper.findAllComponents({ name: 'Icon' })).toHaveLength(7);
  });

});
