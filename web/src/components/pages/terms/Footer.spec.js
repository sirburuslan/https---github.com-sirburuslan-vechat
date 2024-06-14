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
import Footer from './Footer.vue';

// Test the Footer component
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

    test('render elements loading correctly', () => {
        expect(wrapper.find('.vc-footer').exists()).toBeTruthy();
        expect(wrapper.find('.vc-footer > .vc-footer-container').exists()).toBeTruthy();
        expect(wrapper.find('.vc-footer > .vc-footer-container > .vc-footer-terms-links').exists()).toBeTruthy();
        expect(wrapper.findAll('.vc-footer > .vc-footer-container > .vc-footer-terms-links li')).toHaveLength(3);
        expect(wrapper.findAll('.vc-footer > .vc-footer-container > .vc-footer-terms-links a')).toHaveLength(2);
        expect(wrapper.find('.vc-footer > .vc-footer-container > .vc-footer-social-links').exists()).toBeTruthy();
        expect(wrapper.findAll('.vc-footer > .vc-footer-container > .vc-footer-social-links li')).toHaveLength(3);
        expect(wrapper.findAll('.vc-footer > .vc-footer-container > .vc-footer-social-links li a')).toHaveLength(3);
        expect(wrapper.findAll('.vc-footer > .vc-footer-container > .vc-footer-social-links li a .bi')).toHaveLength(3);
    });

});