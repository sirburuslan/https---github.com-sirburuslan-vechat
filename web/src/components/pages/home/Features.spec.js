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
import Features from '@/components/pages/home/Features.vue';

// Test the Features Component
describe('Features', () => {
  let wrapper;

  beforeAll(() => {
    wrapper = mount(Features);
  });

  afterAll(() => {
    wrapper.unmount();
  });

  test('is a Vue instance', () => {
    expect(wrapper.vm).toBeTruthy();
  });

  test('renders elements loading correctly', () => {
    expect(wrapper.find('.vc-features').exists()).toBeTruthy();
    expect(
      wrapper.find('.vc-features > .vc-features-container').exists(),
    ).toBeTruthy();
    expect(
      wrapper.findAll('.vc-features > .vc-features-container > .w-full'),
    ).toHaveLength(2);
    expect(
      wrapper
        .find('.vc-features > .vc-features-container > .w-full > h2')
        .exists(),
    ).toBeTruthy();
    expect(
      wrapper
        .find(
          '.vc-features > .vc-features-container > .w-full > .vc-features-list',
        )
        .exists(),
    ).toBeTruthy();
    expect(
      wrapper.findAll(
        '.vc-features > .vc-features-container > .w-full > .vc-features-list > li',
      ),
    ).toHaveLength(8);
    expect(
      wrapper.findAll(
        '.vc-features > .vc-features-container > .w-full > .vc-features-list > li > h4',
      ),
    ).toHaveLength(8);
    expect(
      wrapper.findAll(
        '.vc-features > .vc-features-container > .w-full > .vc-features-list > li > p',
      ),
    ).toHaveLength(8);
  });

  test('renders icon component', () => {
    expect(wrapper.findAllComponents({ name: 'Icon' })).toHaveLength(8);
  });
});
