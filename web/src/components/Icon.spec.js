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
import Icon from '@/components/Icon.vue';

// Test the Icon Component
describe('Icon', () => {

  test('is a Vue instance', () => {
    const wrapper = mount(Icon, {
      props: { name: 'icon-name' },
    });    
    expect(wrapper.vm).toBeTruthy();
  });

  test('renders props.name when passed', () => {
    const wrapper = mount(Icon, {
      props: { name: 'icon-name' },
    });
    expect(wrapper.text()).toMatch(name);
  });

  test('render props.extraClass when passed', () => {
    const extraClass = 'icon-class';
    const wrapper = mount(Icon, {
      props: { name: 'icon-name', extraClass },
    });
    expect(wrapper.classes()).toContain(extraClass);
  });
});
