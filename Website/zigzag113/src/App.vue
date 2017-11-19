<template>
  <div id="app">
      <navbar></navbar>
      <router-view/>
      <vfooter></vfooter>
  </div>
</template>

<script>
import navbar from './components/Navbar.vue';
import vfooter from './components/Footer.vue';

export default {
  name: 'app',
  components: { navbar, vfooter }
}
</script>
