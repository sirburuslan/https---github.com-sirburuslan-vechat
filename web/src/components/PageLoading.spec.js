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
import PageLoading from '@/components/PageLoading.vue';

// Test the Page Loading Component
describe('PageLoading', () => {
  let wrapper;

  beforeAll(() => {
    wrapper = mount(PageLoading);
  });

  afterAll(() => {
    wrapper.unmount();
  });

  test('is a Vue instance', () => {
    expect(wrapper.vm).toBeTruthy();
  });

  test('renders loading elements correctly', () => {
    expect(wrapper.find('.vc-page-loading').exists()).toBeTruthy();
    expect(wrapper.find('.vc-page-loading > .vc-loading-container').exists()).toBeTruthy();
    expect(wrapper.find('.vc-page-loading > .vc-loading-container > .vc-loading-circle').exists()).toBeTruthy();
    expect(wrapper.findAll('.vc-page-loading > .vc-loading-container > .vc-loading-circle > .vc-loading-circle-box')).toHaveLength(2);
    expect(wrapper.findAll('.vc-page-loading > .vc-loading-container > .vc-loading-circle > .vc-loading-circle-box > .vc-loading-circle-fill')).toHaveLength(2);
    expect(wrapper.find('.vc-page-loading > .vc-loading-container > .vc-loading-circle > .vc-loading-text').exists()).toBeTruthy();
  });  

});
