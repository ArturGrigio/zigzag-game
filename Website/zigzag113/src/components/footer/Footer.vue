<template>
    <span>
        <footer class="page-footer orange">
            <div class="container">
                <div class="row">
                    <div class="col l5 s12">
                        <h5 class="white-text thin">A Little About Shapeless</h5>
                        <p class="grey-text text-lighten-4">Shapeless is a 2D platformer where the player must explore and face various obstacles to save the world of Shapes and Color. Unlike traditional 2D platformer games, Shapeless introduces characters that can be played at the same time within the leve. The Cube, Pyramid, and Sphere each have their own unique traits and abilities, and it is up to player to decide which Shape will best overcome each approaching obstacle.</p>
                    </div>

                    <div class="col offset-l1 l3 s12">
                        <h5 class="white-text thin">Links</h5>
                        <ul>
                            <li v-for="link in links">
                                <a class="white-text" v-bind:href="link.url" v-html="link.text"></a>
                            </li>
                        </ul>
                    </div>
                    <div class="col l3 s12">
                        <h5 class="white-text thin">Creators</h5>
                        <ul>
                            <li><a class="white-text" href="#creators">Thomas Halstead</a></li>
                            <li><a class="white-text" href="#creators">Matt Kenneth Legaspi</a></li>
                            <li><a class="white-text" href="#creators">Daniel Ackerman</a></li>
                            <li><a class="white-text" href="#creators">Artur Grigio</a></li>
                            <li><a class="white-text" href="#creators">Michael Callan</a></li>
                            <li><a class="white-text" href="#creators">Huy Ngo</a></li>
                        </ul>
                    </div>
                </div>
            </div>
            <div class="footer-copyright">
                <div class="container">
                    Made by <a class="orange-text text-lighten-3" href="#">ZigZag</a> UCI Students
                </div>
            </div>
        </footer>
    </span>
</template>

<script>
    import links from '@/components/navbar/links.js';

    export default {
        data: () => {
            return {
                links
            }
        }
    }
</script>
