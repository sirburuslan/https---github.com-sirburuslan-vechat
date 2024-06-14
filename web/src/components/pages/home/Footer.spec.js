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
import Footer from '@/components/pages/home/Footer.vue';

// Test the Footer Component
describe('Footer', () => {
  let wrapper;

  beforeAll(() => {
    wrapper = mount(Footer);
  });

  afterAll(() => {
    wrapper.unmount();
  });

  test('is a Vue instance', () => {
    expect(wrapper.vm).toBeTruthy();
  });

  test('renders element loading correctly', () => {
    expect(wrapper.find('.vc-footer').exists()).toBeTruthy();
    expect(wrapper.find('.vc-footer > .vc-footer-container').exists()).toBeTruthy();
    expect(wrapper.find('.vc-footer > .vc-footer-container > .vc-footer-terms-links').exists()).toBeTruthy();
    expect(wrapper.findAll('.vc-footer > .vc-footer-container > .vc-footer-terms-links > li')).toHaveLength(3);
    expect(wrapper.findAll('.vc-footer > .vc-footer-container > .vc-footer-terms-links > li a')).toHaveLength(2);
    expect(wrapper.find('.vc-footer > .vc-footer-container > .vc-footer-social-links').exists()).toBeTruthy();
    expect(wrapper.findAll('.vc-footer > .vc-footer-container > .vc-footer-social-links > li')).toHaveLength(3);
    expect(wrapper.findAll('.vc-footer > .vc-footer-container > .vc-footer-social-links > li a')).toHaveLength(3);    
  });

  test('renders links loading correctly', () => {
    expect(wrapper.findAll('a').length).toBe(5);
  });
});
